import React from "react";
import { Jumbotron as Jumbo, Container } from "react-bootstrap";
import styled from "styled-components";

const Styles = styled.div`
  .jumbo {
    background: url("https://images.unsplash.com/photo-1541278107931-e006523892df?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1651&q=80")
      no-repeat fixed bottom;
    background-size: cover;
    color: #efefef;
    height: 350px;
    position: relative;
    z-index: -2;
  }
  .overlay {
    background-color: #000;
    opacity: 0.6;
    position: absolute;
    top: 0;
    left: 0;
    bottom: 0;
    right: 0;
    z-index: -1;
  }
`;

export default function Jumbotron() {
  return (
    <Styles>
      <Jumbo fluid className="jumbo">
        <div className="overlay"></div>
        <Container style={{ padding: "45px" }}>
          <h1>Welcome to the future of poker</h1>
          <p>
            Poker is a popular card game around the world, attracting millions
            of players every year along with millions of dollars in prizes.
            While poker sites are not uncommon, our main goal is helping players
            of all levels learn and improve their skills by through constant
            feedback. Using available poker APIs, we integrate automatic
            post-game feedback into our poker game so players can learn about
            their past mistakes and create strategies to improve their winning
            rate. Along with post-game analysis, we also integrate a chat system
            to provide a socializing environment for players to learn from each
            other and share their strategies and stories.
          </p>
        </Container>
      </Jumbo>
    </Styles>
  );
}
