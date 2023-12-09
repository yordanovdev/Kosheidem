import React from "react";
import { MealTypeOverview } from "../../services/client";
import { Meal } from "../Meal/Meal";

interface IMealTypePageProps {
  type: MealTypeOverview;
  weekId: string;
  disableVoting: boolean;
}

export const MealTypePage: React.FC<IMealTypePageProps> = (props) => {
  const { type, weekId, disableVoting } = props;

  return (
    <div className="flex gap-5 flex-wrap px-10 justify-center">
      {type.meals?.map((i) => (
        <Meal
          key={i.id}
          meal={i}
          weekId={weekId}
          disableVoting={disableVoting}
        />
      ))}
    </div>
  );
};
