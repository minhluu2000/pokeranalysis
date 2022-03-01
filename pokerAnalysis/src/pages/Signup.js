import React, { useRef, useState } from "react";
import { Form, Button, Card, Alert } from "react-bootstrap";
import { Container } from "react-bootstrap";
import { useAuth } from "../contexts/AuthContext";
import { Link, useHistory } from "react-router-dom";

function Signup() {
  const emailRef = useRef();
  const passwordRef = useRef();
  const passwordConfirmRef = useRef();
  const { signup } = useAuth();
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState("");
  const history = useHistory();

  async function handleSubmit(e) {

    const delay = ms => new Promise(res => setTimeout(res, ms));

    e.preventDefault();

    if (passwordRef.current.value !== passwordConfirmRef.current.value) {
      return setError("Passwords do not match");
    }

    setMessage("");
    setError("");
    setLoading(true);
    await signup(emailRef.current.value, passwordRef.current.value)
      .then( async () => {
        setMessage("Your account is created! Wait for redirect...")
        await delay(1000);
        history.push("/menu");
      })
      .catch((err) => {
        switch (err.code) {
          case "auth/user-not-found":
            setError("User not found, please sign up.");
            break;
          default:
            setError(err.message);
        }
      });    
      setLoading(false);
  }

  return (
    <>
      <Container
        className="d-flex align-items-center justify-content-center"
        style={{ minHeight: "100vh" }}
      >
        <div className="w-100" style={{ maxWidth: "400px" }}>
          <Card>
            <Card.Body>
              <h2 className="text-center mb-4">Sign Up</h2>
              {error && <Alert variant="danger">{error}</Alert>}
              {message && <Alert variant="success">{message}</Alert>}
              <Form onSubmit={handleSubmit}>
                <Form.Group id="email">
                  <Form.Label>Email</Form.Label>
                  <Form.Control type="email" ref={emailRef} required />
                </Form.Group>
                <Form.Group id="password">
                  <Form.Label>Password</Form.Label>
                  <Form.Control type="password" ref={passwordRef} required />
                </Form.Group>
                <Form.Group id="password-confirm">
                  <Form.Label>Password Confirmation</Form.Label>
                  <Form.Control
                    type="password"
                    ref={passwordConfirmRef}
                    required
                  />
                </Form.Group>
                <Button disabled={loading} className="w-100" type="submit">
                  Sign Up
                </Button>
              </Form>
            </Card.Body>
          </Card>
          <div className="w-100 text-center mt-2">
            Already have an account? <Link to="login">Login</Link>
          </div>
        </div>
      </Container>
    </>
  );
}

export default Signup;
