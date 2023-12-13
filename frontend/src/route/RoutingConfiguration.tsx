import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import { Home } from "../pages/Home/Home";
import { MainLayout } from "./layout/MainLayout";
import { Login } from "../pages/Login/Login";
import { useUser } from "abp-react";

Route.apply;

export const RoutingConfiguration = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<MainLayout />}>
          <Route index element={protect(<Home />)} />
          <Route path="login" element={<Login />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
};

const protect = (component: React.ReactNode) => (
  <ProtectedRoute>{component}</ProtectedRoute>
);

interface IProtectedRoute {
  children: React.ReactNode;
}
const ProtectedRoute: React.FC<IProtectedRoute> = (props) => {
  const user = useUser();

  if (!user) {
    return <Navigate to="/login" />;
  }

  return props.children;
};
