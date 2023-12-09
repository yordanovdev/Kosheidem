import React from "react";
import ReactDOM from "react-dom/client";
import { App } from "./App.tsx";
import { PrimeReactProvider } from "primereact/api";
import { DesignSystem } from "./styles/tailwind/designSystem.ts";
import { QueryClient, QueryClientProvider } from "react-query";

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
    },
  },
});

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <QueryClientProvider client={queryClient}>
      <PrimeReactProvider value={{ unstyled: true, pt: DesignSystem }}>
        <App />
      </PrimeReactProvider>
    </QueryClientProvider>
  </React.StrictMode>
);
