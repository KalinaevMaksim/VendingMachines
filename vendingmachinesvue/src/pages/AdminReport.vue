<template>
    <table class="table table-hover table-striped border-secondary">
        <thead class="">
            <tr>
                <th>Name</th>
                <th>Cost</th>
                <th>Count after last update</th>
                <th>Actual count</th>
                <th>Profit</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="drink in drinks" :key="drink.id">
                <th>{{ drink.idDrinkNavigation.name }}</th>
                <th>{{ drink.idDrinkNavigation.cost }}</th>
                <th>{{ drink.count }}</th>
                <th>{{ drink.count }}</th>
                <th>
                    {{
                        parseInt(drink.count) *
                        parseInt(drink.idDrinkNavigation.cost)
                    }}
                </th>
            </tr>
        </tbody>
    </table>
</template>

<script>
import axios from "axios";
export default {
    data() {
        return {
            drinks: [],
        };
    },

    computed: {
        calcProfit(count, cost) {
            return parseInt(count) * parseInt(cost);
        },
    },

    methods: {
        async getDrinks() {
            this.drinks = (
                await axios.get(
                    `https://localhost:7095/api/VendingMachineDrinks/VendingMachine/${this.$route.params.idVendingMachine}`
                )
            ).data;
        },
    },

    mounted() {
        this.getDrinks();
    },
};
</script>

<style lang="scss" scoped>
</style>