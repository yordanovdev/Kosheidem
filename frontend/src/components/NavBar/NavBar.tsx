import { clearAuthCookies, useUser } from "abp-react";
import { Button } from "primereact/button";
import { useNavigate } from "react-router";

export const NavBar = () => {
  const user = useUser();
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
        <h2 className="text-white text-xl tracking-wide">Kosheidem</h2>
      </div>
      <div>
        {user && (
          <Button label="Log out" severity="secondary" onClick={onLogout} />
        )}
        {!user && <Button label="Login" severity="info" onClick={onLogin} />}
      </div>
    </div>
  );
};
