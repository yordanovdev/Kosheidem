import { GoogleLogin, CredentialResponse } from "@react-oauth/google";
import { tokenAuthService } from "../../services";
import { login } from "abp-react";

export const Login = () => {
  const onExternalGoogleLogin = async (
    credentialResponse: CredentialResponse
  ) => {
    if (!credentialResponse.clientId || !credentialResponse.credential) return;

    const googleLoginResponse = await tokenAuthService.externalAuthenticate({
      authProvider: "Google",
      providerKey: credentialResponse.clientId,
      providerAccessCode: credentialResponse.credential,
    });

    if (
      googleLoginResponse.accessToken &&
      googleLoginResponse.encryptedAccessToken
    ) {
      login({
        accessToken: googleLoginResponse.accessToken,
        encryptedAccessToken: googleLoginResponse.encryptedAccessToken,
        expireInSeconds: googleLoginResponse.expireInSeconds,
      });
      window.location.replace("/");
    }
  };
  return (
    <div className="flex items-center justify-center flex-col gap-3 mt-16">
      <h1 className="text-3xl">Login:</h1>
      <GoogleLogin
        onSuccess={onExternalGoogleLogin}
        onError={() => {
          alert("Login Failed");
        }}
        useOneTap
      />
    </div>
  );
};
