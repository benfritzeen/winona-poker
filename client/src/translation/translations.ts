export const translations = {
  en: {
    pokerPage: {
      title: "Poker Hand Evaluator",
      handTypeLabel: "Hand Type:",
      winner: "Winner!",
      tie: "Tie",
    },
    playerInput: {
      playerLabel: (index: number) => `Player ${index + 1}`,
      playerNameRequired: (index: number) =>
        `Player ${index + 1} name is required`,
      placeholder: "Enter name...",
      dealCards: "Deal Cards",
    },
  },
} as const;

export const t = translations.en;
