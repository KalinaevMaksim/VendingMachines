<template>
    <div class="container-grid">
        <form @submit.prevent>
            <div class="container container__vertical">
                <label class="pt-0">{{ drink.idDrinkNavigation.name }}</label>

                <img
                    class="border border-1 border-secondary p-3"
                    :src="`data:image;base64,${drink.idDrinkNavigation.image}`"
                    alt="image drink"
                />

                <input
                    class="
                        form-control
                        border border-1 border-secondary
                        w-auto
                        mt-3
                    "
                    accept="image/*"
                    type="file"
                    @change="loadImage"
                />

                <label for="inputCost">Цена</label>
                <input
                    type="text"
                    class="border border-1 border-secondary form-control"
                    id="inputCost"
                    v-model="drink.idDrinkNavigation.cost"
                />

                <label for="inputCount">Количество</label>
                <input
                    type="text"
                    class="border border-1 border-secondary form-control"
                    id="inputCount"
                    v-model="drink.count"
                />

                <div class="container mt-3">
                    <button
                        type="submit"
                        class="btn btn-primary"
                        @click="saveDrink"
                    >
                        Сохранить
                    </button>

                    <button
                        type="submit"
                        class="btn btn-primary ms-3"
                        @click="deleteDrink"
                    >
                        Удалить
                    </button>
                </div>
            </div>
        </form>
    </div>
</template>

<script>
import axios from "axios";
export default {
    data() {
        return {
            drink: {
                id: 0,
                idVendingMachine: this.$route.params.idVendingMachine,
                idDrink: 0,
                count: 0,
                idDrinkNavigation: {
                    id: 0,
                    name: "",
                    cost: 0,
                    image: [],
                },
            },
        };
    },

    methods: {
        async getDrink() {
            const parseIdDrink = parseInt(this.$route.params.idDrink);

            if (Object.is(parseIdDrink, NaN) || !parseIdDrink) {
                return;
            }

            this.drink = (
                await axios.get(
                    `https://localhost:7095/api/VendingMachineDrinks/${this.$route.params.idVendingMachine}/${this.$route.params.idDrink}`
                )
            ).data;
        },

        loadImage(event) {
            const file = event.target.files[0];
            const reader = new FileReader();
            
            reader.readAsArrayBuffer(file);

            reader.onload = () => {
                this.drink.idDrinkNavigation.image = Buffer.from(
                    reader.result,
                    "base64"
                ).toString("base64");
            };
        },

        async saveDrink() {
            if (this.drink.idDrinkNavigation.id != 0) {
                await axios.put(
                    `https://localhost:7095/api/Drinks/${this.drink.idDrinkNavigation.id}`,
                    this.drink.idDrinkNavigation
                );
            }

            await axios.put(
                `https://localhost:7095/api/VendingMachineDrinks/${this.drink.id}`,
                this.drink
            );

            this.$router.go(-1);
        },

        async deleteDrink() {
            await axios.delete(
                `https://localhost:7095/api/VendingMachineDrinks/${this.drink.id}`
            );

            this.$router.go(-1);
        },
    },

    created() {
        this.getDrink();
    },
};
</script>

<style lang="scss" scoped>
.container-grid {
    grid-template-columns: 1fr auto;
}

img {
    width: 150px;
    height: 150px;
}
</style>