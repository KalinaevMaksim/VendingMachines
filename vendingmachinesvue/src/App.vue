<template>
    <div class="app border border-2 border-dark">
        <router-view />
    </div>
</template>

<script>
export default {};
</script>

<style lang="scss">
label {
    padding: 15px;
}

img{
    object-fit: contain;
}

button {
    background-color: #383c3d;
    color: white;
    padding: 25px;

    &:disabled {
        background-color: gray;
    }
}

.app {
    font-weight: bold;
    margin: 15px;
    padding: 15px;
    min-width: 400px;

    &.border {
        background-color: #d1dbde;
    }
}

.container {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    align-items: center;
    flex-direction: row;

    &__vertical {
        flex-direction: column;
    }
}

.container-grid {
    display: grid;
    align-items: stretch;
    justify-content: center;
    grid-template-columns: 2fr 1fr;

    &__2 {
        grid-template-columns: 1fr 1fr;
    }

    &__3 {
        grid-template-columns: auto auto auto;
    }
}

.border {
    border-radius: 15px;

    &__dotted {
        border: 2px dotted;
        border-radius: 15px;
        width: 125px;

        &:hover {
            border-color: white;
        }

        &:active {
            background-color: lightgrey;
        }
    }
}
</style>