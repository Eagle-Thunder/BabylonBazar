import React from 'react';
import NavBar from "./NavBar";
import Footer from "./Footer";
import Cart from "./Cart";
import { useEffect, useState } from 'react'

const CartPage = (props) => {
    let [userId, setUserId] = useState(2)

    return (
        <div className="Layout">
            <NavBar />
            <Cart userId={userId} />
            <Footer />
        </div>
    );
}

export default CartPage;