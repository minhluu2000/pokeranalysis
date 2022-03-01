import React from "react";
import { Navbar, Nav } from "react-bootstrap";
import "./Navbar.css";

function Navigation() {
  return (
    <>
      <Navbar className="nav-bar" collapseOnSelect fixed="top" expand="sm">
        <Navbar.Brand href="/">
          <h5 className="nav-bar-brand">
            Aggie Hold'em <i className="fas fa-dog"></i>
          </h5>
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="ml-auto">
            <Nav.Link href="/">
              <h5 className="nav-bar-item">Home</h5>
            </Nav.Link>
            <Nav.Link href="/menu">
              <h5 className="nav-bar-item">Login</h5>
            </Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    </>
  );
}

export default Navigation;
