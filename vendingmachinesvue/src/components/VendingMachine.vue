<template>
    <div
        @click="$router.push(url)"
        class="vending-machine container w-auto container__vertical border__dotted"
    >
        <img
            src="@/assets/images/vending-machine-vector.jpg"
            alt="default image vending machine"
        />
        <label>{{ vendingMachine.id }}</label>
    </div>
</template>

<script>
export default {
    props: {
        vendingMachine: {
            id: 0,
        },
        url: "",
    },
};
</script>

<style lang="scss" scoped>
.vending-machine {
    margin: 15px;
    padding: 15px;

    img {
        height: 100px;
    }
}
</style>