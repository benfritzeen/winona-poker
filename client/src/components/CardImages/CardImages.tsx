import styles from "./CardImages.module.scss";

interface CardImagesProps {
  hand: string[];
}

export const CardImages = ({ hand }: CardImagesProps) => {
  return (
    <div className={styles.cardImages}>
      {hand.map((card) => (
        <img
          key={card}
          className={styles.card}
          src={`/cards/${card}.png`}
          alt={card}
        />
      ))}
    </div>
  );
};
