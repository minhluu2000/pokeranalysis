import React from "react";

import { AuthProvider } from "../contexts/AuthContext";
import { PrivateRoute } from "../contexts/PrivateRoute";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import "./App.css";

import Navigation from "../components/Navbar";
import Footer from "../components/Footer";
import Home from "./Home/Home";
import Analysis from "./Analysis/Analysis";
import Login from "./Login";
import Signup from "./Signup";
import UserMenu from "./UserMenu";
import PokerPage from "./PokerPage/PokerPage";
import ForgotPassword from "./ForgotPassword";
import UpdateProfile from "./UpdateProfile";
import Chat from "./Chat/Chat";

function App() {
  return (
    <div className="App">
      <Navigation />

      <Router>
        <AuthProvider>
          <Switch>
            <Route exact path="/" component={Home} />
            <Route path="/login" component={Login} />
            <Route path="/signup" component={Signup} />
            <Route path="/forgot-password" component={ForgotPassword} />

            <PrivateRoute path="/menu" component={UserMenu} />
            <PrivateRoute path="/update-profile" component={UpdateProfile} />
            <PrivateRoute path="/analysis" component={Analysis} />
            <PrivateRoute path="/play" component={PokerPage} />
            <PrivateRoute path="/chat" component={Chat} />
          </Switch>
        </AuthProvider>
      </Router>

      <Footer />
    </div>
  );
}

export default App;
