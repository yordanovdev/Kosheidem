import { clearAuthCookies, useUser } from "abp-react";
import { Button } from "primereact/button";
import { useNavigate } from "react-router";
import { UserLoginInfoDto } from "../../services/client";
import { defaultUserPhoto } from "../../shared/userConstants";

export const NavBar = () => {
  const user = useUser() as UserLoginInfoDto;
  const navigate = useNavigate();

  const onLogout = () => {
    clearAuthCookies();
    window.location.reload();
  };

  const onLogin = () => {
    navigate("/login");
  };

  return (
    <div className="bg-gray-800 w-full p-6 flex justify-between items-center">
      <div>
        <h2 className="text-white text-2xl tracking-wide">Kosheidem</h2>
      </div>
      <div className="flex items-center gap-5">
        {user && (
          <>
            <Button label="Log out" severity="secondary" onClick={onLogout} />
            <img
              referrerPolicy="no-referrer"
              src={user.picture || defaultUserPhoto}
              className="cursor-pointer w-10 rounded-full object-cover"
            />
          </>
        )}
        {!user && <Button label="Login" severity="info" onClick={onLogin} />}
      </div>
    </div>
  );
};
