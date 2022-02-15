<template>
    <div class="container drink-list">
        <drink-item
            @click="selectDrink(drink)"
            v-model:depositeMoney="this.depositeMoney"
            v-for="drink in drinks"
            :key="drink.id"
            :drink="drink.idDrinkNavigation"
        />

        <div
            id="openFile"
            class="drink-item container container__vertical border__dotted"
        >
            <i class="bi bi-plus"></i>
            <input @change="importDrink" id="inputCSV" type="file" accept=".csv" class="d-none"/>
        </div>
    </div>
</template>

<script>
import DrinkItem from "../components/DrinkItem.vue";
import axios from "axios";

export default {
    components: {
        DrinkItem,
    },

    data() {
        return {
            drinks: [],
        };
    },

    methods: {
        async getDrinks() {
            this.drinks = (
                await axios.get(
                    `https://localhost:7095/api/VendingMachineDrinks/VendingMachine/${this.$route.params.idVendingMachine}`
                )
            ).data;
        },

        selectDrink(drink) {
            this.$router.push(
                `/admin/${this.$route.params.idVendingMachine}/chooseDirectory/drinks/${drink.idDrinkNavigation.id}/drinkEdit`
            );
        },

        importDrink(event) {
            const file = event.target.files[0];

            this.$papa.parse(file, {
                complete: (results) => {
                    console.log(results.data);

                    
                },
            });
        },

        setClickInput() {
            const openFile=document.getElementById("openFile");
            const inputCSV=document.getElementById("inputCSV");

            openFile.addEventListener("click", () => {
                inputCSV.click();
            });
        }
    },

    mounted() {
        this.setClickInput();
        this.getDrinks();
    },
};
</script>

<style lang="scss" scoped>
.bi-plus {
    font-size: 56px;
}

.container {
    .drink-item {
        width: 125px;
        height: 225px;
    }
}
</style>