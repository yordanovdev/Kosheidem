import React, { useMemo, useState } from "react";
import { MealDto } from "../../services/client";
import { Button } from "primereact/button";
import { mealsService } from "../../services";
import { useQueryClient } from "react-query";
import { defaultUserPhoto } from "../../shared/userConstants";
import { useUser } from "abp-react";
import { twMerge } from "tailwind-merge";

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
  const user = useUser();

  const userHasVoted = useMemo(() => {
    const mealVote = meal.users?.find((i) => i.id === user?.id);
    return mealVote;
  }, [meal, user]);

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
    <div className="p-5 shadow-lg w-min flex gap-2 flex-col min-w-[250px] justify-between">
      <div className="flex flex-col gap-0.5">
        <h5 className="text-xl whitespace-nowrap">{meal.name}</h5>
        <p className="italic whitespace-nowrap">
          Votes: {meal.numberOfVotes}
          {meal.votedLastWeek && (
            <span className="text-yellow-500 font-bold"> - Last week</span>
          )}
        </p>
        <div className="flex items-center">
          {meal.users?.map((user, index) => (
            <img
              referrerPolicy="no-referrer"
              key={user.id}
              src={user.picture || defaultUserPhoto}
              className={twMerge(
                "rounded-full w-8 border-black border-2 relative z-1",
                index !== 0 && "left-[-10px]"
              )}
            />
          ))}
        </div>
      </div>
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
