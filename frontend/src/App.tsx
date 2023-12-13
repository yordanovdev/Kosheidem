import { AbpWrapper } from "abp-react";
import { baseUrl, googleClientId, tenantId } from "./services/httpService";
import { RoutingConfiguration } from "./route/RoutingConfiguration";
import "./index.scss";
import { Loading } from "./components/Loading/Loading";
import { GoogleOAuthProvider } from "@react-oauth/google";

export const App = () => {
  return (
    <GoogleOAuthProvider clientId={googleClientId}>
      <AbpWrapper baseUrl={baseUrl} tenantId={tenantId} fallback={<Loading />}>
        <RoutingConfiguration />
      </AbpWrapper>
    </GoogleOAuthProvider>
  );
};
