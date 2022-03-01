import React from "react";
import { Link } from "react-router-dom";
import { Container } from "react-bootstrap";
import { useState, useEffect } from "react";
import "./PokerPage.css";

export default function PokerPage() {
  const [width, setWidth] = useState(960);
  const [height, setHeight] = useState((540 * width) / 960);

  useEffect(() => {
    setHeight((width * 540) / 960);
  }, [width]);

  return (
    <>
      <Container
        className="d-flex align-items-center justify-content-center"
        style={{ minHeight: "100vh" }}
      >
        <div>
          <button
            icon="fas fa-plus"
            rotate={false}
            onClick={() => (width < 1100 ? setWidth(width * 1.1) : "")}
            style={{ opacity: width < 1100 ? 1 : 0.5 }}
          >
            +
          </button>
          <button
            icon="fas fa-plus"
            rotate={false}
            onClick={() => (width > 300 ? setWidth(width / 1.1) : "")}
            style={{ opacity: width > 300 ? 1 : 0.5 }}
          >
            -
          </button>
          <iframe
            className="poker-iframe"
            title="poker-game"
            src="https://i.simmer.io/@Harry0750/pokertest"
            style={{ width: width + "px", height: height + "px" }}
          ></iframe>
          <center>
          <Link to="menu" className="btn btn-primary mt-3" >
            Return to user menu
          </Link>
          </center>
        </div>
      </Container>
    </>
  );
}
