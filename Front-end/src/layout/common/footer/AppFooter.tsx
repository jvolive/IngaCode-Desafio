import Container from "react-bootstrap/Container";

export const AppFooter = () => {
  return (
    <footer className="bg-dark text-white text-center py-3">
      <Container>
        <p className="mb-0 text-center">
          Systems JVO ©{new Date().getFullYear()} Created by João Oliveira
        </p>
      </Container>
    </footer>
  );
};
