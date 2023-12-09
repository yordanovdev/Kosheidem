import { Outlet } from "react-router";
import { NavBar } from "../../components/NavBar/NavBar";

export const MainLayout = () => {
  return (
    <div>
      <NavBar />
      <Outlet />
    </div>
  );
};
