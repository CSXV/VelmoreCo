import { Outlet } from "react-router-dom";
import Footer from "./Footer";
import Header from "./Header";

export default function Layout() {
  return (
    <div className="page-wrapper">
      <Header />

      <main className="main-container">
        <Outlet />
      </main>

      <Footer />
    </div>
  );
}
