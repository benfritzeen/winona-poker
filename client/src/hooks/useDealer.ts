import { useState } from "react";
import { Player } from "../models/Player";

const API_URL = import.meta.env.VITE_API_URL;

export const useDealer = () => {
  const [players, setPlayers] = useState<Player[]>([]);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const dealCards = async (playerNames: string[]) => {
    setLoading(true);
    setError("");

    try {
      const res = await fetch(`${API_URL}/api/poker`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ playerNames }),
      });

      if (!res.ok) throw new Error("API error");

      const data = await res.json();
      setPlayers(Array.isArray(data) ? data : []);
    } catch {
      setError("Error calling API");
    } finally {
      setLoading(false);
    }
  };

  return { players, error, loading, dealCards };
};
