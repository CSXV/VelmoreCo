import "./App.css";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";

import {
  createBrowserRouter,
  createRoutesFromElements,
  Route,
  RouterProvider,
} from "react-router-dom";

import Layout from "./Components/Layout";
import Home from "./Components/Home";
import About from "./Components/About";
import NotFound from "./Components/NotFound";
import Contact from "./Components/Contact";
import Product, { loader as allReviewsLoader } from "./Components/Product";
import Shop, { loader as allProductsLoader } from "./Components/Shop";
import Register, {
  action as registerAction,
} from "./Components/Accounts/Register";
import Login, {
  loader as loginLoader,
  action as loginAction,
} from "./Components/Accounts/Login";
import Account, {
  action as accountAction,
  loader as accountLoader,
} from "./Components/Accounts/Account";
import Cart from "./Components/Cart";

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" element={<Layout />}>
      <Route index element={<Home />} />

      <Route path="about" element={<About />} />
      <Route path="contact" element={<Contact />} />

      <Route path="shop" element={<Shop />} loader={allProductsLoader} />
      <Route path="shop/:id" element={<Product />} loader={allReviewsLoader} />

      <Route
        path="register"
        action={registerAction}
        element={<Register />}
        // errorElement={<Error />}
      />

      <Route
        path="login"
        loader={loginLoader}
        action={loginAction}
        element={<Login />}
        // errorElement={<Error />}
      />

      <Route
        path="cart"
        // loader={loginLoader}
        // action={loginAction}
        element={<Cart />}
        // errorElement={<Error />}
      />

      <Route
        loader={accountLoader}
        action={accountAction}
        path="account/:id"
        element={<Account />}
        // errorElement={<Error />}
      />

      <Route path="*" element={<NotFound />} />
    </Route>,
  ),
);

// ---------------------------------------------------------------------------------
function App() {
  return <RouterProvider router={router} />;
}

export default App;

// <Route
//   loader={loginLoader}
//   action={loginAction}
//   path="login"
//   element={<Login />}
//   errorElement={<Error />}
// />
//
// <Route
//   path="vans"
//   element={<Vans />}
//   loader={vansLoader}
//   errorElement={<Error />}
// />
//
// <Route
//   path="vans/:id"
//   loader={vanDetailLoader}
//   element={<VanDetail />}
//   errorElement={<Error />}
// />
//
// <Route path="host" element={<HostLayout />}>
//   <Route
//     index
//     element={<Dashboard />}
//     loader={async ({ request }) => await requireAuth(request)}
//     errorElement={<Error />}
//   />
//
//   <Route
//     path="income"
//     element={<Income />}
//     loader={async ({ request }) => await requireAuth(request)}
//     errorElement={<Error />}
//   />
//   <Route
//     path="reviews"
//     element={<Reviews />}
//     loader={async ({ request }) => await requireAuth(request)}
//     errorElement={<Error />}
//   />
//
//   <Route
//     path="vans"
//     loader={hostVans}
//     element={<HostVans />}
//     errorElement={<Error />}
//   />
//
//   <Route
//     path="vans/:id"
//     loader={hostVanDetail}
//     element={<HostVanDetail />}
//     errorElement={<Error />}
//   >
//     <Route
//       index
//       element={<HostVanInfo />}
//       loader={async ({ request }) => await requireAuth(request)}
//       errorElement={<Error />}
//     />
//
//     <Route
//       path="pricing"
//       element={<HostVanPricing />}
//       loader={async ({ request }) => await requireAuth(request)}
//       errorElement={<Error />}
//     />
//     <Route
//       path="photos"
//       element={<HostVanPhotos />}
//       loader={async ({ request }) => await requireAuth(request)}
//       errorElement={<Error />}
//     />
//   </Route>
// </Route>
