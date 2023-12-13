import { abp } from "abp-react";
import axios from "axios";

export const baseUrl: string = import.meta.env.VITE_BASE_URL;
export const tenantId: number = import.meta.env.VITE_TENANT_ID;
export const googleClientId: string = import.meta.env.VITE_GOOGLE_API_KEY;

const http = axios.create({
  baseURL: baseUrl,
});

http.interceptors.request.use(
  function (config) {
    if (abp.auth.getToken()) {
      config.headers.Authorization = "Bearer " + abp.auth.getToken();
    }

    config.headers.set(
      ".AspNetCore.Culture",
      abp.utils.getCookieValue("Abp.Localization.CultureName")
    );

    config.headers.set("Abp.TenantId", tenantId);

    return config;
  },

  function (error) {
    return Promise.reject(error);
  }
);

http.interceptors.response.use(
  function (response) {
    response.data = response.data.result;

    try {
      response.data = JSON.stringify(response.data);
    } catch (e) {
      console.error("Error with parsing the response...");
    }
    return response;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export { http };
