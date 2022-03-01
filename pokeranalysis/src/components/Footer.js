import React from "react";
import { Navbar } from "react-bootstrap";
import "./Footer.css";

function Footer() {
  return (
    <>
      <Navbar
        className="nav-bar"
        collapseOnSelect
        fixed="bottom"
        expand="sm"
      >
        <Navbar.Brand href="/"><small className="footer">&copy; Copyright {new Date().getFullYear()}, Project Poker</small></Navbar.Brand>
      </Navbar>
    </>
  );
}

export default Footer;
