import axios from "axios";

export const baseUrl: string = import.meta.env.VITE_BASE_URL;
export const tenantId: number = import.meta.env.VITE_TENANT_ID;

const http = axios.create({
  baseURL: baseUrl,
});

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
