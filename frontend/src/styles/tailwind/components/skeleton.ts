import { SkeletonProps } from "primereact/skeleton";
import { classNames } from "primereact/utils";

interface IRootProps {
  props: SkeletonProps;
}

export const skeletonDesignStyles = {
  root: ({ props }: IRootProps) => ({
    className: classNames(
      "overflow-hidden",
      "!mb-2",
      "bg-gray-300 dark:bg-gray-300",
      "after:absolute after:top-0 after:left-0 after:right-0 after:bottom-0 after:content after:w-full after:h-full after:bg-blue-300 after:left-full after:transform after:translate-x-full after:z-10 after:bg-gradient-to-r after:from-transparent after:via-white after:to-transparent animate-pulse",
      {
        "rounded-md": props.shape !== "circle",
        "rounded-full": props.shape == "circle",
      }
    ),
  }),
};
