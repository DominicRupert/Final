<template>
  <div class="container masonry-with-columns ">
    
    
   

      <div v-for="k in keeps" :key="k.id" class=" p-3 ">
        <Keep :keep="k" />
    </div>
  
   
    </div>
</template>

<script>
import { logger } from '../utils/Logger.js'
import {computed, onMounted, ref} from 'vue'
import {AppState} from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'
export default {
  name: 'Home',
  setup(){  onMounted(async ()=>{
    try{
      await keepsService.getKeeps()
    }catch(e){
      logger.error(e)

    }
  })
  return {
    keeps: computed(()=>AppState.keeps)
  }
  }
}

</script>

<style scoped lang="scss">

.masonry-with-columns {
  margin-top: 20px;
  margin-bottom: 20px;
  display: grid;
  padding: 2em;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  grid-auto-rows: minmax(100px, auto);
}

.home{
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card{
    width: 50vw;
    > img{
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
