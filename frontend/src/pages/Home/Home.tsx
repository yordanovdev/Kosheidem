import React, { useMemo, useState } from "react";
import { weeksService } from "../../services";
import { Week } from "../../components/Week/Week";
import { useQuery } from "react-query";
import { Skeleton } from "primereact/skeleton";

export const Home = () => {
  const [activeIndex, setActiveIndex] = useState<null | number>(null);

  const { data: weeksResponse, isLoading } = useQuery({
    queryKey: ["weeks"],
    queryFn: () => weeksService.getAll(),
  });

  const weeks = useMemo(() => {
    if (!weeksResponse) return [];
    const response = weeksResponse.sort((a, b) =>
      a.startDate < b.startDate ? 1 : -1
    );
    return response;
  }, [weeksResponse]);

  const handleItemClick = (index: number) => {
    setActiveIndex(index === activeIndex ? null : index);
  };

  return (
    <div className="space-y-2 pt-2">
      {isLoading ? (
        <WeeksSkeleton />
      ) : (
        weeks.map((item, index) => (
          <Week
            week={item}
            index={index}
            handleBtnClicked={handleItemClick}
            isOpened={activeIndex === index}
            key={item.id}
          />
        ))
      )}
    </div>
  );
};

const WeeksSkeleton = () => {
  return (
    <React.Fragment>
      {Array.from({ length: 5 }).map((_, index) => (
        <Skeleton
          key={index}
          width="100%"
          height="100px"
          style={{ marginBottom: "10px" }}
        />
      ))}
    </React.Fragment>
  );
};
