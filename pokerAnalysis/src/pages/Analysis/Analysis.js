import React, { useState } from "react";
import SideNav from "./AnalysisComps/Sidenav";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import GamesList from "./AnalysisComps/GamesList";
import BeginnerLesson from "./AnalysisComps/BeginnerLesson";
import IntermediateLesson from "./AnalysisComps/IntermediateLesson";
import AdvancedLesson from "./AnalysisComps/AdvancedLesson";
import User from "./AnalysisComps/User";
import AnalyzeGame from "./AnalysisComps/AnalyzeGame";


function Analysis(){
    return (
        <div>
            <h1 style={{textAlign: "center", color: "#500000"}}>Analysis</h1>
            <div className="row">
                
                   
                    <Router>
                    <div className="col-sm-2">
                        <SideNav/>
                        </div>
                        <div className="col-sm-10">
                        <Switch>
                            <Route exact path="/analysis" render={()=>(<GamesList/>)}/>
                            <Route exact path="/analysis/game" render={(props)=>(<AnalyzeGame {...props}/>)}/>
                        </Switch>
                        </div>
                    </Router>
                
            </div>
        </div>
    )
}
export default Analysis;