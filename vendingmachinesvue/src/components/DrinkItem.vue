<template>
    <div
        :id="`drink-item ${drink.id}`"
        class="drink-item container container__vertical border__dotted w-auto"
    >
        <img :src="`data:image;base64,${drink.image}`" alt="image drink" />
        <label>{{ drink.name }}</label>
        <label>{{ drink.cost }} р.</label>
    </div>
</template>

<script>
export default {
    props: {
        drink: {
            id: 0,
            name: "",
            image: [],
            cost: 0,
        },

        depositeMoney: 0,
    },

    watch: {
        depositeMoney() {
            this.unlockDrink();
        },
    },

    methods: {
        unlockDrink() {
            if (this.depositeMoney === undefined) {
                return;
            }

            const item = document.getElementById(`drink-item ${this.drink.id}`);
            if (this.depositeMoney >= this.drink.cost) {
                item.style["pointer-events"] = "auto";
                item.style["color"] = "black";
            } else {
                item.style["pointer-events"] = "none";
                item.style["color"] = "gray";
            }
        },
    },

    mounted() {
        this.unlockDrink();
    },
};
</script>

<style lang="scss">
.container {
    .drink-item {
        white-space: nowrap;
        margin: 15px;
        padding: 10px 0px 0px 0px;
        min-width: 125px;

        img,
        canvas {
            height: 100px;
            width: 100px;
        }
    }
}
</style>