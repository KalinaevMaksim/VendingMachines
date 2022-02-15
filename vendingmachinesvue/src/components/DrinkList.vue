<template>
    <div class="container drink-list">
        <drink-item
            @click="selectDrink(drink)"
            v-model:depositeMoney="this.depositeMoney"
            v-for="drink in drinks"
            disabled
            :key="drink.id"
            :drink="drink"
        />
    </div>
</template>

<script>
import DrinkItem from "./DrinkItem.vue";

export default {
    props: {
        drinks: {
            type: Array,
            required: true,
        },

        depositeMoney: 0,
    },

    components: {
        DrinkItem,
    },

    methods: {
        selectDrink(drink) {
            this.$emit("selectDrink", drink);
        },
    },
};
</script>

<style lang="scss" scoped>
.container {
    &.drink-list {
        align-content: flex-start;
        justify-content: center;
    }
}


</style>