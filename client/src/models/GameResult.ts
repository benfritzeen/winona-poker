export const GameResult = {
  Win: "Win",
  Tie: "Tie",
  Loss: "Loss",
} as const;

export type GameResultType = (typeof GameResult)[keyof typeof GameResult];
