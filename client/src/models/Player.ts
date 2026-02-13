import { GameResultType } from "./GameResult";

export interface Player {
  name: string;
  hand: string[];
  gameResult: GameResultType;
  handType: string;
}
