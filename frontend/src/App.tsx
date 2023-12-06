import { AbpWrapper } from "abp-react";
import { baseUrl, tenantId } from "./services/httpService";
import { RoutingConfiguration } from "./route/RoutingConfiguration";

export const App = () => {
  return (
    <AbpWrapper baseUrl={baseUrl} tenantId={tenantId}>
      <RoutingConfiguration />
    </AbpWrapper>
  );
};
