import { Link } from "react-router-dom";

function NotFound() {
  return (
    <div className="card">
      <h1 className="fancy-font">404</h1>
      <p>Sorry, the page you were looking for was not found.</p>

      <Link className="button glass" to="/">
        Retrun to home
      </Link>
    </div>
  );
}

export default NotFound;
