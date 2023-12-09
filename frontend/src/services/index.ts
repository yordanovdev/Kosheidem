import { MealsKosheidem, WeeksKosheidem } from "./client";
import { baseUrl, http } from "./httpService";

export const weeksService = new WeeksKosheidem(baseUrl, http);
export const mealsService = new MealsKosheidem(baseUrl, http);
