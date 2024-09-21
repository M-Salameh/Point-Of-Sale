
function addToCart() {
    const storageData = localStorage.getItem(customerId);

    localKey = customerId
    if (storageData) {

        const customerData = JSON.parse(storageData);


        const dataArray = Array.isArray(customerData) ? customerData : [customerData];


        const existingProduct = dataArray.find(item => item.productId === productId);

        if (existingProduct) {

            let productDTO;

            for (var i = 0; i < productDTOList.length; i++) {
                productDTO = productDTOList[i];


                if (parseInt(productDTO.id) == parseInt(productId)) {
                    console.log("the product is in Add");
                    console.log(productDTO);
                    break;
                }

            }
            if (existingProduct.quantity < productDTO.quantity) {

                existingProduct.quantity += 1;
            }
            else {

                
                swal({
                    title: "Sorry Dear User",
                    text: "We Have Only " + productDTO.quantity + " Unit Of The Product " + productDTO.name,
                    icon: "warning",
                    button:"UMMM",
                })
            }

        } else {

            dataArray.push({ productId, quantity: 1 });
        }


        localStorage.setItem(customerId, JSON.stringify(dataArray));
    } else {

        const newData = [{ productId, quantity: 1 }];



        localStorage.setItem(customerId, JSON.stringify(newData));
    }
    console.log(localStorage.getItem(customerId))

    showInfo(productId)
}


function clearCart() {
    localStorage.removeItem(customerId);
    console.log(localStorage.getItem(customerId))
    showInfo(productId)
}


function deleteFromTheCart() {

    const storageData = localStorage.getItem(customerId);

    if (storageData) {
        const customerData = JSON.parse(storageData);

        const updatedData = customerData.filter(item => item.productId !== productId);

        localStorage.setItem(customerId, JSON.stringify(updatedData));
    }
    console.log(localStorage.getItem(customerId))
    showInfo(productId)
}



function decreaseQuantity() {

    const storageData = localStorage.getItem(customerId);

    if (storageData) {

        const customerData = JSON.parse(storageData);


        const productIndex = customerData.findIndex(item => item.productId === productId);

        if (productIndex !== -1) {

            customerData[productIndex].quantity -= 1;

            if (customerData[productIndex].quantity === 0) {

                customerData.splice(productIndex, 1);
            }


            localStorage.setItem(customerId, JSON.stringify(customerData));
        } else {
            console.log(`The product with id = ${productId} didn't exist.`);
        }
    } else {
        console.log(`No data found for customerId: ${customerId}`);
    }
    console.log(localStorage.getItem(customerId))
    showInfo(productId)
}