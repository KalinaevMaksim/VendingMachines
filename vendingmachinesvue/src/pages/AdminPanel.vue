<template>
    <div class="container container__vertical">
        <h1>Вход в систему</h1>

        <h4>Выберите автомат</h4>
        <select
            class="border border-1 border-secondary form-select w-auto"
            v-model="selectedMachineId"
        >
            <option
                v-for="vendingMachine in vendingMachines"
                :key="vendingMachine.id"
                :value="vendingMachine.id"
            >
                {{ vendingMachine.id }}
            </option>
        </select>

        <form @submit.prevent="submit">
            <div class="container">
                <label for="inputPassword">Секретный код</label>
                <input
                    type="password"
                    class="border border-1 border-secondary form-control"
                    id="inputPassword"
                    v-model="secretCode"
                />
                <button type="submit" class="btn btn-primary mt-3">
                    Войти
                </button>
            </div>
        </form>
    </div>
</template>

<script>
import axios from "axios";
import crypto from "crypto";

export default {
    data() {
        return {
            vendingMachines: [],
            selectedMachineId: 0,
            secretCode: "",
        };
    },

    computed: {},

    methods: {
        async getVendingMachines() {
            try {
                const request = await axios.get(
                    "https://localhost:7095/api/VendingMachines"
                );
                this.vendingMachines = request.data;
            } catch (ex) {
                alert(ex);
            }
        },

        async submit() {
            if (this.selectedMachineId == 0) {
                alert("Выберите автомат");
                return;
            }

            const vendingMachine = (
                await axios.get(
                    `https://localhost:7095/api/VendingMachines/${this.selectedMachineId}`
                )
            ).data;

            const hashPassword = crypto
                .createHash("sha256")
                .update(this.secretCode)
                .digest("base64");

            if (
                vendingMachine.secretCode != null &&
                hashPassword != vendingMachine.secretCode
            ) {
                alert("Неправильный пароль");
                return;
            }

            this.$router.push(
                `/admin/${this.selectedMachineId}/chooseDirectory`
            );
        },
    },

    async mounted() {
        this.getVendingMachines();
    },
};
</script>

<style lang="scss" scoped>
</style>