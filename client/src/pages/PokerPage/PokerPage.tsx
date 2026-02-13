import {
  Container,
  Card,
  CardContent,
  Typography,
  CircularProgress,
  Chip,
  Alert,
} from "@mui/material";
import EmojiEventsIcon from "@mui/icons-material/EmojiEvents";
import { PlayerInput } from "../../components/PlayerInput/PlayerInput";
import { CardImages } from "../../components/CardImages/CardImages";
import { useDealer } from "../../hooks/useDealer";
import { GameResult } from "../../models/GameResult";
import { t } from "../../translation/translations";
import styles from "./PokerPage.module.scss";

const getPlayerCardClass = (gameResult: string) => {
  if (gameResult === GameResult.Win)
    return `${styles.playerCard} ${styles.winner}`;
  if (gameResult === GameResult.Tie)
    return `${styles.playerCard} ${styles.tie}`;
  return styles.playerCard;
};

export const PokerPage = () => {
  const { players, error, loading, dealCards } = useDealer();

  return (
    <Container maxWidth="md" className={styles.page}>
      <Typography
        variant="h3"
        component="h1"
        gutterBottom
        className={styles.title}
      >
        {t.pokerPage.title}
      </Typography>

      <div className={styles.formContainer}>
        <PlayerInput onSubmit={dealCards} />
      </div>

      {error && (
        <Alert severity="error" className={styles.error}>
          {error}
        </Alert>
      )}

      {loading && (
        <div className={styles.loading}>
          <CircularProgress />
        </div>
      )}

      <div className={styles.playersList}>
        {players.map((player) => (
          <Card
            key={player.name}
            className={getPlayerCardClass(player.gameResult)}
          >
            <CardContent>
              <div className={styles.playerHeader}>
                <Typography variant="h5" component="h2">
                  {player.name}
                </Typography>
                {player.gameResult === GameResult.Win && (
                  <Chip
                    icon={<EmojiEventsIcon />}
                    label={t.pokerPage.winner}
                    color="warning"
                    size="small"
                  />
                )}
                {player.gameResult === GameResult.Tie && (
                  <Chip
                    label={t.pokerPage.tie}
                    size="small"
                    className={styles.tieChip}
                  />
                )}
              </div>
              <Typography
                variant="body1"
                color="text.secondary"
                className={styles.handType}
              >
                {t.pokerPage.handTypeLabel} <strong>{player.handType}</strong>
              </Typography>
              <CardImages hand={player.hand} />
            </CardContent>
          </Card>
        ))}
      </div>
    </Container>
  );
};
