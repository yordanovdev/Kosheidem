import React, { useMemo, useState } from "react";
import { MealDto } from "../../services/client";
import { Button } from "primereact/button";
import { mealsService } from "../../services";
import { useQueryClient } from "react-query";

interface IMealProps {
  meal: MealDto;
  weekId: string;
  disableVoting: boolean;
}

type StorageMeal = {
  mealId: string;
  weekId: string;
};

export const Meal: React.FC<IMealProps> = (props) => {
  const [loading, setLoading] = useState<boolean>(false);
  const { meal, weekId, disableVoting } = props;
  const queryClient = useQueryClient();

  const userHasVoted = useMemo(() => {
    const userVotesFromStorage = localStorage.getItem("votedMeals");
    const userVotes: StorageMeal[] = userVotesFromStorage
      ? (JSON.parse(userVotesFromStorage) as StorageMeal[])
      : [];

    return userVotes.find((i) => i.mealId === meal.id && i.weekId === weekId)
      ? true
      : false;
  }, [weekId, meal]);

  const onVoteBtnClicked = async () => {
    setLoading(true);
    await mealsService.upVoteMeal({
      weekId: weekId,
      mealId: meal.id,
    });

    const userVotesFromStorage = localStorage.getItem("votedMeals");
    const userVotes: StorageMeal[] = userVotesFromStorage
      ? (JSON.parse(userVotesFromStorage) as StorageMeal[])
      : [];

    userVotes.push({
      mealId: meal.id,
      weekId: weekId,
    });

    localStorage.setItem("votedMeals", JSON.stringify(userVotes));

    await queryClient.refetchQueries({ queryKey: ["week", weekId] });

    setLoading(false);
  };

  const onUnVoteBtnClicked = async () => {
    setLoading(true);
    await mealsService.downVoteMeal({
      weekId: weekId,
      mealId: meal.id,
    });

    const userVotesFromStorage = localStorage.getItem("votedMeals");
    let userVotes: StorageMeal[] = userVotesFromStorage
      ? (JSON.parse(userVotesFromStorage) as StorageMeal[])
      : [];

    userVotes = userVotes.filter(
      (i) => i.mealId !== meal.id && i.weekId !== weekId
    );
    localStorage.setItem("votedMeals", JSON.stringify(userVotes));

    await queryClient.refetchQueries({ queryKey: ["week", weekId] });

    setLoading(false);
  };

  return (
    <div className="p-5 shadow-lg w-min flex gap-2 flex-col min-w-[250px]">
      <h5 className="text-xl whitespace-nowrap">{meal.name}</h5>
      <p className="italic">Votes: {meal.numberOfVotes}</p>
      <Button
        label={userHasVoted ? "UnVote" : "Vote"}
        className="w-min"
        icon="fa fa-check"
        onClick={userHasVoted ? onUnVoteBtnClicked : onVoteBtnClicked}
        disabled={loading || disableVoting}
        severity={userHasVoted ? "danger" : "info"}
      />
    </div>
  );
};
