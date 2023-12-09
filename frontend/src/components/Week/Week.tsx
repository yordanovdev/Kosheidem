import { Button } from "primereact/button";
import { WeekOverviewDto } from "../../services/client";
import { twMerge } from "tailwind-merge";
import { useMemo, useState } from "react";
import { useQuery } from "react-query";
import { mealsService } from "../../services";
import { WeekContent } from "./WeekContent";

interface IWeek {
  week: WeekOverviewDto;
  isOpened: boolean;
  index: number;
  handleBtnClicked: (item: number) => void;
}

export const Week: React.FC<IWeek> = (props) => {
  const { week, isOpened, handleBtnClicked, index } = props;
  const [activeIdx, setActiveIdx] = useState(0);

  const { data: mealsResponse } = useQuery({
    queryKey: ["week", week.id],
    queryFn: () => mealsService.getMealsByType(week.id),
    enabled: week && isOpened,
  });

  const types = useMemo(() => {
    if (!mealsResponse) return;
    return mealsResponse;
  }, [mealsResponse]);

  const readableStatus = useMemo(() => {
    if (week.current) return "Current";
    if (week.future) return "Future";
    return "Past";
  }, [week]);

  const statusSeverity = useMemo(() => {
    if (week.current) return "info";
    if (week.future) return "help";
    return "danger";
  }, [week]);

  return (
    <div
      className={twMerge(
        "bg-white border rounded-md overflow-hidden transition-shadow ease-in-out duration-300",
        week.future && "bg-gray-200 pointer-events-none cursor-pointer"
      )}
    >
      <div
        className="flex items-center justify-between cursor-pointer p-8"
        onClick={() => handleBtnClicked(index)}
      >
        <div className="flex gap-5 items-center flex-wrap">
          <span className="text-lg font-semibold">{`${formatDate(
            week.startDate
          )} - ${formatDate(week.endDate)}`}</span>
          <Button label={readableStatus} severity={statusSeverity} />
        </div>

        {!week.future && (
          <span className="text-xl">{isOpened ? "[-]" : "[+]"}</span>
        )}
      </div>
      <div
        className={`p-4 ${
          isOpened ? "block opacity-100" : "hidden opacity-0"
        } `}
      >
        {types && (
          <WeekContent
            activeIdx={activeIdx}
            setActiveIdx={setActiveIdx}
            types={types}
            weekId={week.id}
            disableVoting={week.past ? true : false}
          />
        )}
      </div>
    </div>
  );
};

const formatDate = (item: Date): string => {
  const date = new Date(item);
  return date.toLocaleDateString("en-US", {
    day: "numeric",
    month: "long",
    year: "numeric",
  });
};
