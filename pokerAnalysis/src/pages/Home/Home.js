import React from "react";
import Jumbotron from "./Jumbotron.js";


import "./Home.css";

export default function Home() {
  return (
    <>
      <Jumbotron />

      <div className="about hide-on-small-only">
        <div className="row">
          <div className="col s12 m3">
            <img
              className="responsive-img"
              src={
                "https://images.unsplash.com/photo-1596451190630-186aff535bf2?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1653&q=80"
              }
              alt="bg"
              height="100px"
              width="150px"
            />
            <h5 className="font">Play</h5>
            <p className="para">
              Through constant playing, players can actively gain new experiences and improve their skills quickly.
            </p>
            <h6>
              <b>Embrace the challenge.</b>
            </h6>
          </div>
          <div className="col s12 m3">
            <img
              className="responsive-img"
              src={
                "https://images.unsplash.com/photo-1604345250885-11f528eec3ff?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1693&q=80"
              }
              alt="bg"
              height="100px"
              width="150px"
            />
            <h5 className="font">Learn</h5>
            <p className="para">
              Learn from mistakes, view previous games' stats and learn the pattern to improve.
            </p>
            <h6>
              <b>Level up your skills.</b>
            </h6>
          </div>
          <div className="col s12 m3">
            <img
              className="responsive-img"
              src={
                "https://images.unsplash.com/photo-1518133227682-c0e3e34de21b?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1334&q=80"
              }
              alt="bg"
              height="100px"
              width="150px"
            />
            <h5 className="font">Community</h5>
            <p className="para">
              Share your stories and experiences with other. Build great friendships. Improve together
            </p>
            <h6>
              <b>Rise up together.</b>
            </h6>
          </div>
          <div className="col s12 m3">
            <img
              className="responsive-img"
              src={
                "https://images.unsplash.com/photo-1511193311914-0346f16efe90?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=1653&q=80"
              }
              alt="bg"
              height="100px"
              width="150px"
            />
            <h5 className="font">Free</h5>
            <p className="para">
              Everyone has the same equal chance to become a great player. No micro-transaction. No member fee. No hidden cost. Just fun. 
            </p>
            <h6>
              <b>No money, no problem.</b>
            </h6>
          </div>
        </div>
      </div>
    </>

    // TODO: Add new details here
  );
}
