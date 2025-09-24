import { useLoaderData } from "react-router-dom";
import ShopItems from "./ShopItems";
import { getAllProducts } from "../Api";

export async function loader() {
  return { Products: getAllProducts() };
}

export default function Shop() {
  const allProductsData = useLoaderData();

  return (
    <>
      <div className="card top-card shop-cards">
        <h1>Summer Jewellery Selection</h1>
        <p>
          LOVE bracelets, Juste un Clou rings, Trinity earrings: the Maison's
          jewellery icons like to shine in the sun.
        </p>
      </div>

      <div className="shop-container">
        <ShopItems params={allProductsData} />
      </div>
    </>
  );
}
