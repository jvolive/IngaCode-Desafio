import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import { Link } from "react-router-dom";
import LoginModal from "../../../components/login/LoginModal";

export const AppHeader = () => {
  return (
    <Navbar bg="dark" variant="dark" className="py-3">
      <Container>
        <Navbar.Brand as={Link} to="/">
          IngaCode - Desafio
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link as={Link} to="/projects">
              Projetos
            </Nav.Link>
            <Nav.Link as={Link} to="/tasks">
              Tarefas
            </Nav.Link>
          </Nav>
          <LoginModal />
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};
