import "bootstrap/dist/css/bootstrap.min.css";
import { AppHeader } from "../common";
import { AppFooter } from "../common/footer/AppFooter";
import { ReactNode } from "react";

interface MainLayoutProps {
  children: ReactNode;
}

export function MainLayout({ children }: MainLayoutProps) {
  return (
    <div className="d-flex flex-column min-vh-100">
      <AppHeader />
      <main role="main" className="container mt-4 flex-grow-1">
        {children}
      </main>
      <AppFooter />
    </div>
  );
}
