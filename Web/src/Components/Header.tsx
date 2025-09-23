import { NavLink } from "react-router-dom";

function Header() {
  const loggedIn = localStorage.getItem("isLoggedIn");

  function handleLogout() {
    localStorage.setItem("isLoggedIn", "false");
    localStorage.setItem("userID", "0");
  }

  return (
    <header className="header-container glass">
      <NavLink className="fancy-font" to="/">
        Velmoré & Co
      </NavLink>

      <nav className="nav-conatiner">
        <NavLink className="fancy-font" to="shop">
          Shop
        </NavLink>
        <NavLink className="fancy-font" to="about">
          About
        </NavLink>

        <NavLink to="/cart">
          <span className="fancy-font">Cart</span>
        </NavLink>

        {loggedIn == "true" ? (
          <NavLink to={`/account/${localStorage.getItem("userID")}`}>
            <span className="fancy-font">Account</span>
          </NavLink>
        ) : (
          <NavLink className="fancy-font" to="login">
            Login
          </NavLink>
        )}

        {loggedIn == "true" ? (
          <NavLink to="/login">
            <span className="fancy-font" onClick={handleLogout}>
              logout
            </span>
          </NavLink>
        ) : null}
      </nav>
    </header>
  );
}

export default Header;
