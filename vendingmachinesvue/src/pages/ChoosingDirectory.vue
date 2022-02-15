<template>
    <div class="container container__vertical">
        <h1>Административная панель</h1>
        <h5 @click="routeDirectory('coins')">Монеты</h5>
        <h5 @click="routeDirectory('drinks')">Напитки</h5>
        <h5 @click="routeDirectory('report')">Отчёт</h5>
    </div>
</template>

<script>
export default {
    methods: {
        routeDirectory(directory) {
            this.$router.push(
                `/admin/${this.$route.params.idVendingMachine}/chooseDirectory/${directory}`
            );
        },
    },
};
</script>

<style lang="scss" scoped>
h5 {
    &:hover {
        cursor: pointer;
    }
}
</style>