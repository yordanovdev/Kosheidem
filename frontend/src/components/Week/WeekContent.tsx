import React, { useMemo } from "react";
import { twMerge } from "tailwind-merge";
import { MealTypeOverview } from "../../services/client";
import { MealTypePage } from "../MealTypePage/MealTypePage";

interface IWeekContentProps {
  activeIdx: number;
  setActiveIdx: React.Dispatch<React.SetStateAction<number>>;
  types: MealTypeOverview[];
  weekId: string;
  disableVoting: boolean;
}

export const WeekContent: React.FC<IWeekContentProps> = (props) => {
  const { activeIdx, setActiveIdx, types, weekId, disableVoting } = props;

  const currentType = useMemo(() => {
    return types?.[activeIdx];
  }, [activeIdx, types]);

  return (
    currentType && (
      <div className="flex flex-col items-center gap-5">
        <div className="flex gap-5 items-center shadow-md p-5 px-10 rounded-sm justify-between min-w-[300px]">
          <i
            className={twMerge(
              "fa fa-angle-left cursor-pointer text-xl",
              activeIdx === 0 && "text-gray-400"
            )}
            onClick={() => setActiveIdx((idx) => (idx !== 0 ? idx - 1 : 0))}
          />
          <h3 className="font-bold text-xl">{currentType.type}</h3>
          <i
            className={twMerge(
              "fa fa-angle-right cursor-pointer text-xl",
              activeIdx === types.length - 1 && "text-gray-400"
            )}
            onClick={() =>
              setActiveIdx((idx) =>
                idx !== types.length - 1 ? idx + 1 : types.length - 1
              )
            }
          />
        </div>

        <MealTypePage
          type={currentType}
          weekId={weekId}
          disableVoting={disableVoting}
        />
      </div>
    )
  );
};
