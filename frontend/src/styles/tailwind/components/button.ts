import { classNames } from "primereact/utils";
import { ButtonProps } from "primereact/button";

interface IRootProps {
  props: ButtonProps;
}

export const buttonDesignStyles = {
  root: ({ props }: IRootProps) => ({
    className: classNames(
      "items-center cursor-pointer inline-flex overflow-hidden relative select-none text-center align-bottom h-[40px]",
      "transition duration-200 ease-in-out",
      "focus:outline-none focus:outline-offset-0 focus:shadow-[0_0_0_2px_rgba(255,255,255,1),0_0_0_4px_rgba(157,193,251,1),0_1px_2px_0_rgba(0,0,0,1)] dark:focus:shadow-[0_0_0_2px_rgba(28,33,39,1),0_0_0_4px_rgba(147,197,253,0.7),0_1px_2px_0_rgba(0,0,0,0)]", // Primary button focus
      {
        "text-white bg-blue-500 border border-blue-500 hover:bg-blue-600 hover:border-blue-600":
          !props.link &&
          props.severity === null &&
          !props.text &&
          !props.outlined &&
          !props.plain,
        "text-blue-600 bg-transparent border-transparent": props.link,
      },
      {
        "text-white bg-gray-500 border border-gray-500 hover:bg-gray-600 hover:border-gray-600":
          props.severity === "secondary" &&
          !props.text &&
          !props.outlined &&
          !props.plain,
        "text-white bg-green-500 border border-green-500 hover:bg-green-600 hover:border-green-600":
          props.severity === "success" &&
          !props.text &&
          !props.outlined &&
          !props.plain,
        "text-white bg-blue-500 border border-blue-500 hover:bg-blue-600 hover:border-blue-600":
          props.severity === "info" &&
          !props.text &&
          !props.outlined &&
          !props.plain,
        "text-white bg-orange-500 border border-orange-500 hover:bg-orange-600 hover:border-orange-600":
          props.severity === "warning" &&
          !props.text &&
          !props.outlined &&
          !props.plain,
        "text-white bg-purple-500 border border-purple-500 hover:bg-purple-600 hover:border-purple-600":
          props.severity === "help" &&
          !props.text &&
          !props.outlined &&
          !props.plain,
        "text-white bg-red-500 border border-red-500 hover:bg-red-600 hover:border-red-600":
          props.severity === "danger" &&
          !props.text &&
          !props.outlined &&
          !props.plain,
      },
      { "shadow-lg": props.raised },
      { "rounded-md": !props.rounded, "rounded-full": props.rounded },
      {
        "bg-transparent border-transparent": props.text && !props.plain,
        "text-blue-500 hover:bg-blue-300/20":
          props.text &&
          (props.severity === null || props.severity === "info") &&
          !props.plain,
        "text-gray-500 hover:bg-gray-300/20":
          props.text && props.severity === "secondary" && !props.plain,
        "text-green-500 hover:bg-green-300/20":
          props.text && props.severity === "success" && !props.plain,
        "text-orange-500 hover:bg-orange-300/20":
          props.text && props.severity === "warning" && !props.plain,
        "text-purple-500 hover:bg-purple-300/20":
          props.text && props.severity === "help" && !props.plain,
        "text-red-500 hover:bg-red-300/20":
          props.text && props.severity === "danger" && !props.plain,
      },
      { "shadow-lg": props.raised && props.text },
      {
        "text-gray-500 hover:bg-gray-300/20": props.plain && props.text,
        "text-gray-500 border border-gray-500 hover:bg-gray-300/20":
          props.plain && props.outlined,
        "text-white bg-gray-500 border border-gray-500 hover:bg-gray-600 hover:border-gray-600":
          props.plain && !props.outlined && !props.text,
      },
      {
        "bg-transparent border": props.outlined && !props.plain,
        "text-blue-500 border border-blue-500 hover:bg-blue-300/20":
          props.outlined &&
          (props.severity === null || props.severity === "info") &&
          !props.plain,
        "text-gray-500 border border-gray-500 hover:bg-gray-300/20":
          props.outlined && props.severity === "secondary" && !props.plain,
        "text-green-500 border border-green-500 hover:bg-green-300/20":
          props.outlined && props.severity === "success" && !props.plain,
        "text-orange-500 border border-orange-500 hover:bg-orange-300/20":
          props.outlined && props.severity === "warning" && !props.plain,
        "text-purple-500 border border-purple-500 hover:bg-purple-300/20":
          props.outlined && props.severity === "help" && !props.plain,
        "text-red-500 border border-red-500 hover:bg-red-300/20":
          props.outlined && props.severity === "danger" && !props.plain,
      },
      {
        "px-4 py-3 text-base": props.size === null,
        "text-xs py-2 px-3": props.size === "small",
        "text-xl py-3 px-4": props.size === "large",
      },
      { "opacity-60 pointer-events-none cursor-default": props.disabled }
    ),
  }),
  label: ({ props }: IRootProps) => ({
    className: classNames("flex-1", "duration-200", "font-bold", {
      "hover:underline": props.link,
    }),
  }),
  icon: ({ props }: IRootProps) => ({
    className: classNames("mx-0", {
      "mr-2": props.iconPos == "left" && props.label != null,
      "ml-2": props.iconPos == "right" && props.label != null,
      "mb-2": props.iconPos == "top" && props.label != null,
      "mt-2": props.iconPos == "bottom" && props.label != null,
    }),
  }),
  badge: ({ props }: IRootProps) => ({
    className: classNames({
      "ml-2 w-4 h-4 leading-none flex items-center justify-center": props.badge,
    }),
  }),
};
