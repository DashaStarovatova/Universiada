import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import App from "./App";
import { Cabinet } from "./Cabinet";
import "./index.css";
// npm install keycloak-js @react-keycloak-fork/web

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <React.StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<App />} />
        <Route path="/cabinet" element={<Cabinet />} />
      </Routes>
    </BrowserRouter>
  </React.StrictMode>
);