import { AbpWrapper } from "abp-react";
import { baseUrl, tenantId } from "./services/httpService";
import { RoutingConfiguration } from "./route/RoutingConfiguration";
import "./index.scss";
import { Loading } from "./components/Loading/Loading";

export const App = () => {
  return (
    <AbpWrapper baseUrl={baseUrl} tenantId={tenantId} fallback={<Loading />}>
      <RoutingConfiguration />
    </AbpWrapper>
  );
};
