import React, { useState } from "react";
import { Link } from "react-router-dom";
import "./Sidenav.css";

function SideNav() {
  const [click, setClick] = useState(false);

  const handleClick = () => setClick(!click);
  const closeMobileMenu = () => setClick(false);

  return (
    <>
      <div style={{color: "#200000"}}>
              <ul className="nav flex-column">
              <Link to="/analysis" className="nav-link" onClick={closeMobileMenu}>
              <li className="nav-item" >              
                Games List
              </li>
              </Link>
              </ul>
      </div>
    </>
  );
}

export default SideNav;
