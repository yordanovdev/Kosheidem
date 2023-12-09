import { PrimeReactPTOptions } from "primereact/api";
import { buttonDesignStyles } from "./components/button";
import { progressSpinnerDesignStyles } from "./components/progressSpinner";
import { skeletonDesignStyles } from "./components/skeleton";
import { globalCss } from "./globalCss";

export const DesignSystem: PrimeReactPTOptions = {
  global: { css: () => globalCss },
  button: buttonDesignStyles,
  skeleton: skeletonDesignStyles,
  progressspinner: progressSpinnerDesignStyles,
};
