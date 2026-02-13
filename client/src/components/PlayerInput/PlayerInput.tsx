import { useForm, useFieldArray, Controller } from "react-hook-form";
import { TextField, Button, Stack } from "@mui/material";
import { t } from "../../i18n/translations";
import styles from "./PlayerInput.module.scss";

interface PlayerFormData {
  players: { name: string }[];
}

interface PlayerInputProps {
  onSubmit: (playerNames: string[]) => void;
}

export const PlayerInput = ({ onSubmit }: PlayerInputProps) => {
  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<PlayerFormData>({
    defaultValues: {
      players: [{ name: "" }, { name: "" }],
    },
  });

  const { fields } = useFieldArray({
    control,
    name: "players",
  });

  const handleFormSubmit = (data: PlayerFormData) => {
    onSubmit(data.players.map((p) => p.name));
  };

  return (
    <form
      className={styles.playerInput}
      onSubmit={handleSubmit(handleFormSubmit)}
    >
      <Stack spacing={2}>
        {fields.map((field, index) => (
          <div key={field.id} className={styles.fieldRow}>
            <Controller
              name={`players.${index}.name`}
              control={control}
              rules={{ required: t.playerInput.playerNameRequired(index) }}
              render={({ field }) => (
                <TextField
                  {...field}
                  label={t.playerInput.playerLabel(index)}
                  placeholder={t.playerInput.placeholder}
                  size="small"
                  fullWidth
                  error={!!errors.players?.[index]?.name}
                  helperText={errors.players?.[index]?.name?.message}
                />
              )}
            />
          </div>
        ))}

        <Button variant="contained" type="submit">
          {t.playerInput.dealCards}
        </Button>
      </Stack>
    </form>
  );
};
