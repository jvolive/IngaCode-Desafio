import "bootstrap/dist/css/bootstrap.min.css";
import { AppHeader } from "../common";
import { AppFooter } from "../common/footer/AppFooter";

export function MainLayout() {
  return (
    <div>
      <AppHeader />
      <main role="main" className="container mt-4"></main>
      <AppFooter />
    </div>
  );
}
