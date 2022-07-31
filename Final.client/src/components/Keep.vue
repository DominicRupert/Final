<template>
    <div class="card bg-dark">
        <h1>{{keep.name.description}}</h1>


    </div>
</template>


<script>
import { AppState } from '../AppState.js'
import { useRouter } from 'vue-router'
export default {
    props: {
        keep: {
            type: Object,
            required: true
        }
    },
    setup(){
        return {
            // vaults: computed(()=>AppState.vaults),
            keeps: computed(()=>AppState.keeps),
        }
    }
}
</script>


<style lang="scss" scoped>

</style>